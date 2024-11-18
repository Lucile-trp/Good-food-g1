import React from "react";
import { UserProvider } from "@/contexts/UserContext";
import { CartProvider } from "@/contexts/CartContext";

interface AppProvidersProps {
  children: React.ReactNode;
}

const AppProviders: React.FC<AppProvidersProps> = ({ children }) => {
  return (
    <UserProvider>
      <CartProvider>
        {children}     
      </CartProvider>
    </UserProvider>
  );
};

export default AppProviders;
