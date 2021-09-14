import { useCartContext } from "../../../context/cart";
import Moment from "moment";
import { useEffect } from "react";

function Edd() {
  const { cart, loadingCart } = useCartContext();

  return (
    <div className="w-full">
      <div className="flex flex-wrap mb-3">
        {!cart.allCartEvents.includes("ShippingEddCalculated") ||
        loadingCart ? (
          <pre className="animate-pulse w-full h-6 bg-gray-200 rounded"></pre>
        ) : (
          <pre className="font-sans font-bold px-3">
            Arrives {Moment(cart.expectedEddStart).format("ddd., MMM DD")}{" "}
            -&nbsp;
            {Moment(cart.expectedEddEndtoLocaleDateString).format(
              "ddd., MMM DD"
            )}
          </pre>
        )}
      </div>
      <div className="flex flex-wrap mb-3">
        <img className="w-full md:w-1/2" src="/shoe.jpg" />
        <div className="w-full md:w-1/2 flex flex-col">
          <pre className="w-2 text-wrap font-sans">
            Trainer
            <br />
            Genome Men's
            <br />
            shoe
          </pre>
          <pre className="font-sans text-gray-400">
            Qty {cart.products[0].quantity}
          </pre>
          <pre className="font-sans text-gray-400">Size US 7.5</pre>
          <pre className="font-sans text-gray-400">
            ${cart.products[0].grossPrice}
          </pre>
        </div>
      </div>
    </div>
  );
}

export default Edd;
