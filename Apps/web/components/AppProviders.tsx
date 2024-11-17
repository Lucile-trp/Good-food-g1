import React from "react";
import { UserProvider } from "@/contexts/UserContext";

interface AppProvidersProps {
  children: React.ReactNode;
}

const AppProviders: React.FC<AppProvidersProps> = ({ children }) => {
  return (
    <UserProvider>
        {children}
    </UserProvider>
  );
};

export default AppProviders;
