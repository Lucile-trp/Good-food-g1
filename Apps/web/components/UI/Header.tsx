// components/Header.tsx

import Image from "next/image";
import React from "react";

interface HeaderProps {
  isConnected: boolean;
}

const Header: React.FC<HeaderProps> = ({ isConnected }) => {
  return (
    <header className="flex items-center justify-between  bg-white border border-gray-200 md:px-8">
      <h1 className="text-2xl font-bold text-black">GOODFOOD</h1>

      <nav className="flex items-center">
        {!isConnected ? (
          <div className="pt-4">
            <button className="px-4 py-2 text-white bg-black border-r hover:bg-gray-800">
              Se connecter
            </button>
            <button className="px-4 py-2 text-white bg-black hover:bg-gray-800">
              S'inscrire
            </button>
          </div>
        ) : (
          <div className="flex pt-4 items-end">
            <button className="px-4 py-2 text-white bg-black border-r hover:bg-gray-800 h-10">
              Panier
            </button>
            <button className="px-4 py-2 text-white bg-black hover:bg-gray-800 h-10">
              <Image
                src="/icons/png/white/user_icon.png"
                width={20}
                height={20}
                alt="User icon picture"
              ></Image>
            </button>
          </div>
        )}
      </nav>
    </header>
  );
};

export default Header;
