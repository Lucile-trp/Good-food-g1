export type Product = {
    id: number;
    title: string;
    description: string;
    cost: number;
};

export const mockProducts: Product[] = [
    {
        id: 1,
        title: "Pizza",
        description: "Une pizza classique avec de la sauce tomate, de la mozzarella et du basilic frais.",
        cost: 8.99
    },
    {
        id: 2,
        title: "Burger Gourmet",
        description: "Un burger avec un steak juteux, du fromage cheddar, et une sauce maison.",
        cost: 12.49
    },
    {
        id: 3,
        title: "Pâtes Carbonara",
        description: "Pâtes crémeuses accompagnées de lardons et de parmesan.",
        cost: 9.99
    },
    {
        id: 4,
        title: "Salade César",
        description: "Salade fraîche avec du poulet grillé, des croûtons et une sauce César.",
        cost: 7.49
    },
    {
        id: 5,
        title: "Sushi Assorti",
        description: "Un assortiment de sushis variés, préparés avec des ingrédients frais.",
        cost: 14.99
    },
    {
        id: 6,
        title: "Tacos au Poulet",
        description: "Tacos garnis de poulet mariné, de légumes frais et de sauce piquante.",
        cost: 6.99
    },
    {
        id: 7,
        title: "Steak Frites",
        description: "Un steak tendre accompagné de frites croustillantes.",
        cost: 15.99
    },
    {
        id: 8,
        title: "Soupe Pho",
        description: "Une soupe vietnamienne parfumée avec des nouilles de riz et du bœuf.",
        cost: 10.49
    },
    {
        id: 9,
        title: "Wrap Végétarien",
        description: "Un wrap garni de légumes grillés, de houmous et de salade croquante.",
        cost: 6.49
    },
    {
        id: 10,
        title: "Tiramisu",
        description: "Un dessert italien classique avec des couches de mascarpone et de biscuits imbibés de café.",
        cost: 5.99
    }
];
