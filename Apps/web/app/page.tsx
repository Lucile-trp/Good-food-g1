import Link from "next/link";

export default function Home() {
  return (
    <main className="grow flex flex-col items-center justify-center">
      {/* HOME SECTION */}
      <section>
        <div className="absolute left-0 bg-[#D9D9D9] h-64 w-full"></div>

        {/* GRID */}
        <div className="grid grid-cols-4 gap-4 mt-6">
          <div className="col-span-4 lg:col-span-4 lg:col-start-2 relative">
            <div>
              <img
                src="/images/img1.jpg"
                alt="Plat en vedette"
                className="w-full h-auto relative z-10"
              />
              <div className="w-full h-full bg-black absolute top-1 left-1 z-0"></div>
            </div>
          </div>
        </div>
        {/* ABSOLUTE */}
        <div className="absolute top-12 bg-opacity-80 h-full content-center z-20">
          <h1 className="text-5xl font-extrabold text-black leading-tight text-stroke-white">
            BIENVENUE SUR GOODFOOD
          </h1>
          <div className="mt-6 flex flex-col gap-2">
            <label
              htmlFor="restaurant"
              className="block text-lg font-medium mb-2 text-black"
            >
              Choisissez votre restaurant
            </label>
            <input
              id="restaurant"
              type="text"
              placeholder="Restaurants"
              className="w-full px-4 py-2 border border-dark_gray rounded focus:outline-none"
            />
            <Link href="/products">
              <button className="px-6 py-2 bg-black text-white rounded w-32">
                Accéder
              </button>
            </Link>
          </div>
        </div>
      </section>

      <div className="h-px w-[75%] bg-dark_gray mt-10" />

      {/* DELIVERY SECTION */}
      <section className="">
        <div className="my-10 mx-auto flex gap-8">
          <div className="relative">

            <img
              src="/images/img2.jpg"
              alt="Livreur"
              className="w-full h-auto relative z-10"
            />
            <div className="w-full h-full absolute z-0 top-1 left-1 bg-black"></div>
          </div>
          <div className="">
            <section className="relative w-fit">
              <h1 className="relative z-10 text-black">Livrez avec nous !</h1>
              <div className="absolute z-0 top-7 left-7 h-[20px] w-full bg-secondary_green"></div>
            </section>

            <p className="text-black max-w-xl	">
              But I must explain to you how all this mistaken idea of denouncing
              pleasure and praising pain was born and I will give you a complete
              account of the system, and expound the actual teachings of the
              great explorer of the truth, the master-builder of human
              happiness. No one rejects, dislikes, or avoids pleasure itself,
              because it is pleasure, but because those who do not know how to
              pursue pleasure rationally encounter consequences that are
              extremely painful.
            </p>
          </div>
        </div>
      </section>
    </main>
  );
}
