function Confirmation({ onConfirm }) {
  const handleConfirm = (evt) => {
    evt.preventDefault();
    onConfirm();
  };

  return (
    <div>
      <form className="w-[512px] max-w-lg">
        <div className="flex flex-wrap mb-3">
          <pre className="font-sans text-xl px-3">
            Please confirm your order
          </pre>
        </div>
        <div className="flex flex-wrap mb-3 justify-center">
          <button
            className="rounded-full py-3 px-6 mt-6 bg-[#171717] hover:bg-gray-700 w-full text-white"
            onClick={handleConfirm}
          >
            Confirm
          </button>
        </div>
      </form>
    </div>
  );
}

export default Confirmation;
