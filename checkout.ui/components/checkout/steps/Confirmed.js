import { useCartContext } from "../../../context/cart";

function Confirmed({ onTryAgain }) {
  const { cart, loadingOrderReference } = useCartContext();

  const handleTryAgain = (evt) => {
    evt.preventDefault();
    onTryAgain();
  };

  return (
    <div>
      <form className="w-[512px] max-w-lg">
        {loadingOrderReference ? (
          <div className="flex flex-wrap mb-3">
            <pre className="font-sans text-xl px-3">
              Your order is being confirmed
            </pre>
            <pre className="font-sans px-3 animate-pulse w-full h-6 bg-gray-200"></pre>
          </div>
        ) : (
          <div className="flex flex-wrap mb-3">
            <pre className="font-sans text-xl px-3">
              Your order has been confirmed
            </pre>
            <pre className="font-sans px-3">
              Order reference: {cart.retailerOrderReference}
            </pre>
            <button
              className="rounded-full py-3 px-6 mt-6 bg-[#171717] hover:bg-gray-700 w-full text-white"
              onClick={handleTryAgain}
            >
              Try again
            </button>
          </div>
        )}
      </form>
    </div>
  );
}

export default Confirmed;
