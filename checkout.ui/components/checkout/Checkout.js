import { useCartContext } from "../../context/cart";
import { useState } from "react";
import { useRouter } from "next/router";

import Edd from "./summary/Edd";
import Price from "./summary/Price";

import Shopper from "./steps/Shopper";
import Shipping from "./steps/Shipping";
import Payment from "./steps/Payment";
import Confirmation from "./steps/Confirmation";
import Confirmed from "./steps/Confirmed";

function Checkout() {
  const [step, setStep] = useState("shopper");
  const { cart, dispatch } = useCartContext();
  const router = useRouter();

  const provideShopperDetailsUrl = `http://localhost:5000/api/cart/${cart.id}/shopperdetails`;
  const provideShippingDetailsUrl = `http://localhost:5000/api/cart/${cart.id}/shippingdetails`;
  const confirmCartUrl = `http://localhost:5000/api/cart/${cart.id}/confirm`;

  async function submitShopperDetails(shopper) {
    dispatch({ type: "cartLoading" });
    const response = await fetch(provideShopperDetailsUrl, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(shopper),
    });
    await response.json();
    setStep("shipping");
  }

  async function submitShippingDetails(shipping) {
    dispatch({ type: "cartLoading" });
    const response = await fetch(provideShippingDetailsUrl, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(shipping),
    });
    await response.json();
    setStep("payment");
  }

  async function confirmOrder() {
    dispatch({ type: "orderReferenceLoading" });
    const response = await fetch(confirmCartUrl, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ id: cart.id }),
    });
    await response.json();
    setStep("confirmed");
  }

  function payOrder() {
    setStep("confirmation");
  }

  function tryAgain() {
    router.push("/");
  }

  return (
    <div className="flex flex-row justify-center">
      {step === "shopper" && <Shopper onSubmit={submitShopperDetails} />}
      {step === "shipping" && <Shipping onSubmit={submitShippingDetails} />}
      {step === "payment" && <Payment onPaid={payOrder} />}
      {step === "confirmation" && <Confirmation onConfirm={confirmOrder} />}
      {step === "confirmed" && <Confirmed onTryAgain={tryAgain} />}
      <div className="flex flex-col">
        <Price />
        <Edd />
      </div>
    </div>
  );
}

export default Checkout;
