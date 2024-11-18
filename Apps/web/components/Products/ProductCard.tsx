import { useCart } from "@/contexts/CartContext";
import { Product } from "@/types/product";

export const ProductCard = ({ product }: { product: Product }) => {
  const {addToCart} = useCart();
  return (
    <div className="w-full h-full relative w-fit" id={product.Id.toString()}>
      <div className="relative border bg-white z-20">
        <div className="grid grid-cols-2">
          <div className="w-full h-full bg-black">
            <img
              src="/images/img1.jpg"
              alt="Plat en vedette"
              className="w-full h-auto border-r"
            />
          </div>
          <div className="p-2 flex flex-col justify-between h-full ">
            <h4>{product.titre}</h4>
            <p>{product.description}</p>
            <p>{product.prix}e</p>

            <button className="bg-black text-white p-2 rounded" onClick={() => addToCart(product)}>
              Ajouter au panier
            </button>
          </div>
        </div>
      </div>

      <div className="absolute w-full h-full bg-black top-1 left-1 z-0"></div>
    </div>
  );
};
