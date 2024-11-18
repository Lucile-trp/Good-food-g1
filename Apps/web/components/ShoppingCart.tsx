"use client";

import { useCart } from "@/contexts/CartContext";
import { Dispatch, SetStateAction } from "react";

export const ShoppinCart = ({
  setOpenCart,
}: {
  setOpenCart: Dispatch<SetStateAction<boolean>>;
}) => {
  const { cart, removeFromCart } = useCart();
  return (
    <div className="fixed flex flex-col right-0 top-0 z-[9999] w-1/3 h-full bg-white border justify-between p-3">
      {/* Header */}
      <div className="flex justify-between">
        <div
          className="bg-error rounded h-6 w-6 flex justify-center items-center cursor-pointer"
          onClick={() => setOpenCart(false)}
        >
          <p className="text-white">X</p>
        </div>
        <h2>Votre panier</h2>
        <div></div>
      </div>
      {/* BODY */}
      <div className="h-full">
        {cart.length !== 0 ? (
          cart.map((e) => {
            return (
              <div className="m-2" key={e.Id.toString()}>
                <h2>{e.titre}</h2>
                <p>{e.description}</p>
                <p>{e.prix}e</p>
                <button
                  className="h-8 w-full bg-error text-white rounded"
                  onClick={() => removeFromCart(e.Id)}
                >
                  Supprimer du panier
                </button>

                <div className="h-px w-full bg-dark_gray my-4 relative z-10" />
              </div>
            );
          })
        ) : (
          <div className="border bg-white_smoke p-5 mt-5">
            <p>
              <strong>Votre panier est vide.</strong>
            </p>
          </div>
        )}
      </div>

      {/* FOOTER */}
      <button className="h-8 w-full bg-black rounded text-white">
        Valider la commande
      </button>
    </div>
  );
};
