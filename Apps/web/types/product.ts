export type Product = {
    id: number;
    titre: string;
    description: string;
    prix: number;
};

export const mockProducts: Product[] = [
    {
        id: 1,
        titre: "Pizza Margherita",
        description: "Une pizza classique avec de la sauce tomate, de la mozzarella et du basilic frais.",
        prix: 8.99
    },
    {
        id: 2,
        titre: "Burger Gourmet",
        description: "Un burger avec un steak juteux, du fromage cheddar, et une sauce maison.",
        prix: 12.49
    },
    {
        id: 3,
        titre: "Pâtes Carbonara",
        description: "Pâtes crémeuses accompagnées de lardons et de parmesan.",
        prix: 9.99
    },
    {
        id: 4,
        titre: "Salade César",
        description: "Salade fraîche avec du poulet grillé, des croûtons et une sauce César.",
        prix: 7.49
    },
    {
        id: 5,
        titre: "Sushi Assorti",
        description: "Un assortiment de sushis variés, préparés avec des ingrédients frais.",
        prix: 14.99
    },
    {
        id: 6,
        titre: "Tacos au Poulet",
        description: "Tacos garnis de poulet mariné, de légumes frais et de sauce piquante.",
        prix: 6.99
    },
    {
        id: 7,
        titre: "Steak Frites",
        description: "Un steak tendre accompagné de frites croustillantes.",
        prix: 15.99
    },
    {
        id: 8,
        titre: "Soupe Pho",
        description: "Une soupe vietnamienne parfumée avec des nouilles de riz et du bœuf.",
        prix: 10.49
    },
    {
        id: 9,
        titre: "Wrap Végétarien",
        description: "Un wrap garni de légumes grillés, de houmous et de salade croquante.",
        prix: 6.49
    },
    {
        id: 10,
        titre: "Tiramisu",
        description: "Un dessert italien classique avec des couches de mascarpone et de biscuits imbibés de café.",
        prix: 5.99
    }
];
