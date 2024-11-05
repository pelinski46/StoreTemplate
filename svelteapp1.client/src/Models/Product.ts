import type {Category} from "./Category";

export interface Product {
    id?: number;
    title?: string | undefined;
    description?: string | undefined;
    quantity?: number;
    price?: number;
    categoryId?: number;
    category?: Category;
    image?: string | undefined;
}