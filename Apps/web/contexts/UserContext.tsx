'use client'

import React, { createContext, useContext, useEffect, useState } from "react";

interface User {
  id: string;
  email: string;
  role: string; 
}

interface UserContextProps {
  user: User | null; 
  setUser: (user: User | null) => void; 
  logout: () => void; 
}

// CONTEXT
const UserContext = createContext<UserContextProps | undefined>(undefined);

// PROVIDER
export const UserProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [user, setUser] = useState<User | null>(null);

  const logout = () => setUser(null);

  useEffect(() => {
    console.log("context user : ", user);

  },[user])

  return (
    <UserContext.Provider value={{ user, setUser, logout }}>
      {children}
    </UserContext.Provider>
  );
};

// HOOK 
export const useUser = () => {
  const context = useContext(UserContext);
  if (!context) {
    throw new Error("useUser must be used within a UserProvider");
  }
  return context;
};
