"use client";

import { ProductCard } from "@/components/Products/ProductCard";
import { mockProducts, Product } from "@/types/product";
import { useState } from "react";

export default function ProductPage() {
  const [entries, setEntries] = useState([]);
  const [plats, setPlats] = useState<Product[]>(mockProducts);
  const [desserts, setDesserts] = useState([]);

  return (
    <main className="grow flex flex-col text-black px-4 md:px-8 xl:px-64 relative">
      <div className="absolute left-0 bg-[#D9D9D9] h-64 w-full z-0"></div>
      {/* HOME SECTION */}
      <section className="relative z-10">
        {/* GRID */}
        <div className="">
          <h1>VOTRE RESTAURANT</h1>
          <input list="cities" />
          <datalist id="cities">
            <option value="Liste des villes" />
          </datalist>
        </div>
      </section>

      {/* ENTRIES */}
      <section className="relative z-10 py-5">
        <div className="relative w-fit">
          <h1 className="relative z-10">Les entrées</h1>
          <div className="absolute z-0 top-7 left-7 h-[20px] w-full bg-secondary_rose"></div>
        </div>

        {entries.length !== 0 ? (
          entries.map((o) => {
            return <></>;
          })
        ) : (
          <p>Arrive prochainement 🚀</p>
        )}
      </section>

      <div className="h-px w-full bg-dark_gray mt-10 relative z-10" />

      {/* PLATS */}
      <section className="relative z-10 py-5">
        <div className="relative w-fit">
          <h1 className="relative z-10">Les plats</h1>
          <div className="absolute z-0 top-7 left-7 h-[20px] w-full bg-secondary_green"></div>
        </div>
        <div className="grid grid-cols-1 gap-5 md:grid-cols-2 lg:grid-cols-3">
          {plats.length !== 0 ? (
            plats.map((o) => {
              return (
                <ProductCard product={o} key={o.id.toString()}></ProductCard>
              );
            })
          ) : (
            <p>Pas de plats disponibles.</p>
          )}
        </div>
      </section>

      <div className="h-px w-full bg-dark_gray mt-10" />

      {/* DESSERTS */}
      <section className="relative z-10 py-5">
        <div className="relative w-fit">
          <h1 className="relative z-10">Les desserts</h1>
          <div className="absolute z-0 top-7 left-7 h-[20px] w-full bg-secondary_yellow"></div>
        </div>
        {desserts.length !== 0 ? (
          desserts.map((o) => {
            return <></>;
          })
        ) : (
          <p>Arrive prochainement 🚀</p>
        )}
      </section>
    </main>
  );
}
