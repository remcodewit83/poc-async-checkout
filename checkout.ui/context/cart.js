import { createContext, useContext } from "react";

const CartContext = createContext();

export function CartWrapper({ data, children }) {
  return <CartContext.Provider value={data}>{children}</CartContext.Provider>;
}

export function useCartContext() {
  return useContext(CartContext);
}
