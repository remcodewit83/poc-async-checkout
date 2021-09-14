import { useCartContext } from "../../../context/cart";

function Price() {
  const { cart, loadingCart } = useCartContext();
  return (
    <div>
      <div className="flex flex-wrap mb-3">
        <pre className="font-sans text-xl px-3">Order Summary</pre>
      </div>
      <div className="flex flex-wrap mb-3">
        <pre className="w-full md:w-1/2 font-sans px-3 text-gray-400">
          Subtotal
        </pre>
        {!cart.allCartEvents.includes("PriceCalculated") || loadingCart ? (
          <pre className="animate-pulse w-full md:w-1/2 h-6 bg-gray-200 rounded text-right"></pre>
        ) : (
          <pre className="w-full md:w-1/2 font-sans px-3 text-gray-400 text-right">
            ${cart.netPrice}
          </pre>
        )}
      </div>
      <div className="flex flex-wrap mb-3">
        <pre className="w-full md:w-1/2 font-sans px-3 text-gray-400">Tax</pre>
        {!cart.allCartEvents.includes("PriceCalculated") || loadingCart ? (
          <pre className="animate-pulse w-full md:w-1/2 h-6 bg-gray-200 rounded text-right"></pre>
        ) : (
          <pre className="w-full md:w-1/2 font-sans px-3 text-gray-400 text-right">
            ${cart.tax}
          </pre>
        )}
      </div>
      <div className="flex flex-wrap mb-3">
        <pre className="w-full md:w-1/2 font-sans px-3 text-gray-400">
          Delivery / shipping
        </pre>
        {!cart.allCartEvents.includes("PriceCalculated") || loadingCart ? (
          <pre className="animate-pulse w-full md:w-1/2 h-6 bg-gray-200 rounded text-right"></pre>
        ) : (
          <pre className="w-full md:w-1/2 font-sans px-3 text-gray-400 text-right">
            ${cart.shippingCost}
          </pre>
        )}
      </div>
      <div className="flex flex-wrap mb-3 border-t-2 border-b-2 pt-4 pb-4">
        <pre className="w-full md:w-1/2 font-sans px-3">Total</pre>
        {!cart.allCartEvents.includes("PriceCalculated") || loadingCart ? (
          <pre className="animate-pulse w-full md:w-1/2 h-6 bg-gray-200 rounded text-right"></pre>
        ) : (
          <pre className="w-full md:w-1/2 font-sans px-3 text-right">
            ${cart.totalPrice}
          </pre>
        )}
      </div>
    </div>
  );
}

export default Price;
