export const Footer = () => {
  const FooterNavigation = [
    "Obtenir de l’aide",
    "Créer un compte",
    "Livrer",
    "Mention légale",
  ];
  return (
    <footer className="py-12 bg-dark_gray text-gray-300 text-white md:px-8 xl:px-64 ">
      <div className="max-w-7xl mx-auto grid grid-cols-2 md:grid-cols-4 gap-8">
        {FooterNavigation.map((title, index) => (
          <div key={index}>
            <h4 className="font-bold mb-4">{title}</h4>
            <ul className="font-light">
              <li className="text-sm">Lien 1</li>
              <li className="text-sm">Lien 2</li>
              <li className="text-sm">Lien 3</li>
            </ul>
          </div>
        ))}
      </div>
      <div className="text-center mt-8 text-sm font-light">
        Copyright - GoodFood - contact@emailgoodfood.com - 2023
      </div>
    </footer>
  );
};
