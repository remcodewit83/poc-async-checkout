using CorrelationId;
using CorrelationId.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MassTransit;
using MassTransit.KafkaIntegration;
using Application.Consumers;
using Application.Domain.Events;
using Application.Store;
using Application.UseCases.CreateCart;
using Application.Domain;
using Application.UseCases.GetCartDetails;
using Application.Hubs;
using Application.UseCases.ProvideShopperDetails;
using Application.UseCases.ProvideShippingDetails;
using System.Text.Json.Serialization;
using Application.UseCases.RequestToConfirmCart;
using Application.UseCases.ConfirmCart;
using Api.Extensions;
using Application.Providers;
using Application.UseCases.CalculatePrice;
using Application.UseCases.CalculateEdd;

namespace checkout.api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.UsingInMemory((context, cfg) => cfg.ConfigureEndpoints(context));
                x.AddRider(rider =>
                {
                    rider.AddConsumer<KafkaConsumer>();
                    rider.AddProducer<CartUpdated>(Configuration["Kafka:Producer:Topic"]);
                    rider.UsingKafka((context, k) =>
                    {
                        k.Host(Configuration["Kafka:Host"]);
                        k.TopicEndpoint<CartUpdated>(Configuration["Kafka:Consumer:Topic"], Configuration["Kafka:Consumer:GroupId"], e =>
                        {
                            e.AutoOffsetReset = Confluent.Kafka.AutoOffsetReset.Latest;
                            e.ConfigureConsumer<KafkaConsumer>(context);
                            e.CreateIfMissing(t =>
                            {
                                t.NumPartitions = 3;
                                t.ReplicationFactor = 1;
                            });
                        });
                    });
                });
            });

            services.AddMassTransitHostedService(true); // wait for the buscontrol to start

            services.AddMediator(cfg =>
            {
                cfg.AddConsumer<CreateCartConsumer>();
                cfg.AddConsumer<ProvideShopperDetailsConsumer>();
                cfg.AddConsumer<ProvideShippingDetailsConsumer>();
                cfg.AddConsumer<GetCartDetailsConsumer>();
                cfg.AddConsumer<CalculatePriceConsumer>();
                cfg.AddConsumer<CalculateEddConsumer>();
                cfg.AddConsumer<RequestToConfirmCartConsumer>();
                cfg.AddConsumer<ConfirmCartConsumer>();
            });

            services.AddSingleton((_) => new LatencyProvider());
            services.AddTransient<IEddProvider, EddProvider>();
            services.AddTransient<IPriceProvider, PriceProvider>();
            services.AddTransient<IRetailerProvider, RetailerProvider>();
            services.AddSingleton((_) => new MongoDbContext(Configuration["MongoDb:ConnectionString"]));
            services.AddTransient<ICartStore, CartStore>();

            services.AddDefaultCorrelationId();
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            services.AddAutoMapper(typeof(Startup).Assembly);
            services.AddAutoMapper(typeof(Cart).Assembly);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v5", new OpenApiInfo { Title = "checkout.api", Version = "v5" });
            });
            services.AddCors(o => o.AddPolicy("checkout", builder =>
            {
                builder
                    .WithOrigins("http://localhost:3000") // Wildcard is not allowed for the origin when using SignalR
                    .AllowAnyHeader()
                    .WithMethods("GET", "POST")
                    .AllowCredentials();
            }));
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v5/swagger.json", "checkout.api v5"));
            }
            app.UseCors("checkout");
            app.UseRouting();
            app.UseCorrelationId();
            app.UseErrorHandler();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<StatusHub>("/status");
            });
        }
    }
}
