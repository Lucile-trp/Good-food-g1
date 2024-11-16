export default function Home() {
  return (
    <main className="grow bg-background flex flex-col items-center justify-center">
      
      {/* HOME SECTION */}
      <section>
      <div className="absolute left-0 bg-[#D9D9D9] h-64 w-full"></div>
        
        {/* GRID */}
        <div className="grid grid-cols-4 gap-4 mt-6">
          <div className="col-span-4 lg:col-span-4 lg:col-start-2 relative">
            <img
              src="/images/img1.jpg"
              alt="Plat en vedette"
              className="w-full h-auto object-cover rounded shadow-lg border border-gray-300"
            />
          </div>
        </div>
        {/* ABSOLUTE */}
        <div className="absolute top-12 bg-opacity-80 h-full content-center">
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
            <button className="px-6 py-2 bg-black text-white rounded w-32">
              Accéder
            </button>
          </div>
        </div>
      </section>

      <div className="h-px w-[75%] bg-dark_gray mt-10"/>

      {/* DELIVERY SECTION */}
      <section className="">
        <div className="my-10 mx-auto flex gap-8">
          <div className="flex-1">
            <img
              src="/images/img2.jpg"
              alt="Livreur"
              className="w-full h-auto rounded shadow"
            />
          </div>
          <div className="flex-1">
            <h2 className="text-5xl font-bold mb-4 text-black">
              Livrez avec nous !
            </h2>
            <p className="text-black">
              But I must explain to you how all this mistaken idea of denouncing
              pleasure...
            </p>
          </div>
        </div>
      </section>

    </main>
  );
}
