// components/Header.tsx
"use client";

import Image from "next/image";
import Link from "next/link";
import React, { useState } from "react";
import { AuthModale } from "../Modales/AuthModale";
import { useUser } from "@/contexts/UserContext";
import { ShoppinCart } from "../ShoppingCart";
import { useCart } from "@/contexts/CartContext";

const Header: React.FC = () => {
  const [authModale, setAuthModale] = useState<boolean>(false);
  const [authUserChoice, setAuthUserChoice] = useState<string>("");
  const [openCart, setOpenCart] = useState<boolean>(false);
  const { cart } = useCart();

  const { user, setUser } = useUser();

  function handleDisconnected() {
    setUser(null);
  }
  return (
    <header className="flex items-center justify-between bg-white border-b border-dark_gray md:px-8 xl:px-64 text-sm">
      <Link href="/">
        <Image
          src="/logo/png/black/logo_black.png"
          height={50}
          width={180}
          alt="Logo Goodfood"
        ></Image>
      </Link>

      <nav className="flex items-center">
        {!user ? (
          <div className="pt-4">
            <button
              className="px-4 py-2 text-white bg-black border-r hover:bg-secondary_purple"
              onClick={() => (setAuthModale(true), setAuthUserChoice("SignIn"))}
            >
              Se connecter
            </button>
            <button
              className="px-4 py-2 text-white bg-black hover:bg-secondary_green"
              onClick={() => (setAuthModale(true), setAuthUserChoice("SignUp"))}
            >
              S'inscrire
            </button>
          </div>
        ) : (
          <div className="flex pt-4 items-end">
            <button
              className="px-4 py-2 text-white bg-black border-r hover:bg-secondary_green h-10 flex gap-2 items-center"
              onClick={() => {
                setOpenCart(true);
              }}
            >
              Panier
              <div className="h-6 w-6 bg-white_smoke rounded text-black">
                {cart && cart.length}
              </div>
            </button>
            <button className="px-4 py-2 text-white bg-black  border-r hover:bg-secondary_purple h-10">
              <Image
                src="/icons/png/white/user_icon.png"
                width={20}
                height={20}
                alt="User icon picture"
              ></Image>
            </button>
            <button
              className="px-4 py-2 text-white bg-black hover:bg-secondary_purple h-10"
              onClick={() => {
                handleDisconnected();
              }}
            >
              Déconnexion
            </button>
          </div>
        )}
      </nav>

      {/* Modales */}

      {/* Auth */}
      <div className="fixed top-0 left-0 z-[9999]">
        {authModale ? (
          <AuthModale
            setAuthModale={setAuthModale}
            authUserChoice={authUserChoice}
            setAuthUserChoice={setAuthUserChoice}
          />
        ) : (
          <></>
        )}
      </div>
      {/* Basket */}
      {openCart && <ShoppinCart setOpenCart={setOpenCart}></ShoppinCart>}
    </header>
  );
};

export default Header;
