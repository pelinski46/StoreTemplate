<script lang="ts">
    import { Card, Button, Rating, Badge } from 'flowbite-svelte';
    import type { Category } from "../Models/Category";
    import type { Product } from "../Models/Product";
    import { onMount } from "svelte";

    // Categories
    let categories: Category[] = [];
    let selectedCategoryId: number | null = null;
    
    // Products
    let products: Product[] = [];
    let loading = false;
    let error: Error | null = null;

    // Fetch categories
    async function fetchCategories() {
        const response = await fetch('/Categories');
        if (!response.ok) throw new Error('Failed to fetch categories');
        return (await response.json()).$values;
    }

    // Fetch products by category
    async function fetchProductsByCategory(categoryId: number): Promise<Product[]> {
        const response = await fetch(`/Product/bycategory/${categoryId}`);
        if (!response.ok) throw new Error(`Failed to fetch products for category ${categoryId}`);
        return (await response.json()).$values;
    }

    // Fetch all products
    async function fetchAllProducts(): Promise<Product[]> {
        const response = await fetch('/Product');
        if (!response.ok) throw new Error('Failed to fetch products');
        return (await response.json()).$values;
    }

    // Handle category selection
    function handleCategorySelection(categoryId: number) {
        selectedCategoryId = selectedCategoryId === categoryId ? null : categoryId;
    }

    // Reactive statement to handle product fetching
    $: {
        if (selectedCategoryId !== null) {
            loadFilteredProducts(selectedCategoryId);
        } else {
            loadAllProducts();
        }
    }

    async function loadAllProducts() {
        try {
            loading = true;
            products = await fetchAllProducts();
            error = null;
        } catch (e) {
            error = e as Error;
            products = [];
        } finally {
            loading = false;
        }
    }

    async function loadFilteredProducts(categoryId: number) {
        try {
            loading = true;
            const fetchedProducts = await fetchProductsByCategory(categoryId);
            console.log("Fetched Products:", fetchedProducts); // <-- Revisa en la consola
            products = fetchedProducts;
            error = null;
        } catch (e) {
            error = e as Error;
            products = [];
        } finally {
            loading = false;
        }
    }

    // Load initial data
    onMount(async () => {
        try {
            loading = true;
            categories = await fetchCategories();
            await loadAllProducts();
        } catch (e) {
            error = e as Error;
        } finally {
            loading = false;
        }
    });
</script>

<style>
    .product-grid {
        display: grid;
        grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
        gap: 20px;
    }
    
    .category-item.selected {
        background-color: #f3f4f6;
    }
</style>

<div class="flex flex-col lg:flex-row">
    <!-- Category Filter Sidebar -->
    <aside class="w-full lg:w-64 h-screen p-4 bg-white border-r border-gray-200">
        <h2 class="text-xl font-bold mb-4">Filter by Category</h2>
        <div class="space-y-2">
            {#each categories as category}
                <div 
                    class="flex items-center p-2 hover:bg-gray-50 rounded cursor-pointer {selectedCategoryId === category.id ? 'selected' : ''}"
                    on:click={() => handleCategorySelection(category.id)}
                >
                    <input 
                        type="radio" 
                        id={category.id.toString()} 
                        checked={selectedCategoryId === category.id}
                        class="w-4 h-4 text-blue-600 bg-gray-100 border-gray-300 rounded-full focus:ring-blue-500"
                    />
                    <label 
                        for={category.id.toString()} 
                        class="ms-2 text-sm font-medium text-gray-900"
                    >
                        {category.name}
                    </label>
                </div>
            {/each}
        </div>
    </aside>

    <!-- Main Content -->
    <main class="flex-1 min-h-screen bg-gray-50">
        {#if loading}
            <div class="p-8 text-center">Loading products...</div>
        {:else if error}
            <div class="p-8 text-red-600">Error: {error.message}</div>
        {:else if products.length === 0}
            <div class="p-8 text-center text-gray-500">
                {selectedCategoryId ? 'No products found in this category' : 'No products available'}
            </div>
        {:else}
            <div class="p-4 lg:p-8">
                <div class="product-grid grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-5">
                    {#each products as product}
                        <Card padding="none">
                            <a href="/">
                                <img class="p-8 rounded-t-lg" src={product.image || '/images/default-product.jpg'} 
                                     alt={product.title} />
                            </a>
                            <div class="px-5 pb-5">
                                <a href="/">
                                    <h5 class="text-xl font-semibold tracking-tight text-gray-900">
                                        {product.title}
                                    </h5>
                                </a>
                                <Rating rating={product.rating || 0} size={24} class="mt-2.5 mb-5">
                                    <Badge slot="text" class="ms-3">{product.rating || 'N/A'}</Badge>
                                </Rating>
                                <div class="flex justify-between items-center">
                                    <span class="text-3xl font-bold text-gray-900">
                                        ${product.price.toFixed(2)}
                                    </span>
                                    <Button href="/">Buy now</Button>
                                </div>
                            </div>
                        </Card>
                    {/each}
                </div>
            </div>
        {/if}
    </main>
</div>