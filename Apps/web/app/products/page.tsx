"use client";

import { ProductCard } from "@/components/Products/ProductCard";
import { Product } from "@/types/product";
import { useEffect, useState } from "react";

export default function ProductPage() {
  const [entries, setEntries] = useState([]);
  const [plats, setPlats] = useState<Product[]>([]);
  const [desserts, setDesserts] = useState([]);

  useEffect(() => {
    setPlats([
      { Id: 1, titre: "test", description: "test", prix: 5 },
      { Id: 2, titre: "test", description: "test", prix: 5 },
      { Id: 3, titre: "test", description: "test", prix: 5 },
      { Id: 4, titre: "test", description: "test", prix: 5 },
      { Id: 5, titre: "test", description: "test", prix: 5 },
      { Id: 6, titre: "test", description: "test", prix: 5 },
    ]);
  }, []);

  return (
    <main className="grow flex flex-col text-black md:px-8 xl:px-64 relative">
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
        <div className="grid grid-cols-3 gap-5">
          {plats.length !== 0 ? (
            plats.map((o) => {
              console.log(o);
              return <ProductCard product={o}></ProductCard>;
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
