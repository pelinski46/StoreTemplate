<script lang="ts">
    import { Card, Button, Rating, Badge } from 'flowbite-svelte';
    import type {Category} from "../Models/Category";
    import {onMount} from "svelte";

    let categories: Category[] = [];

    async function fetchCategories() {
        console.log('Fetching products...');
        const response = await fetch('/Categories'); // Adjusted URL if needed
        console.log('Response status:', response.status); // Log response status

        if (!response.ok) {
            throw new Error('Failed to fetch products');
        }

        try {
            const data = await response.json();
            console.log('Fetched data:', data); // Log fetched data
            return data.$values; // Return the array from $values
        } catch (error) {
            console.error('Error parsing JSON:', error);
            throw new Error('Failed to parse products data');
        }
    }


    onMount(async () => {
        try {
            categories = await fetchCategories(); // Fetch and store categories
        } catch (error) {
            console.error('Error during mount:', error);
        }
    });
    async function getProducts() {
        console.log('Fetching products...');
        const response = await fetch('/Product'); // Adjusted URL if needed
        console.log('Response status:', response.status); // Log response status

        if (!response.ok) {
            throw new Error('Failed to fetch products');
        }

        try {
            const data = await response.json();
            console.log('Fetched data:', data); // Log fetched data
            return data.$values; // Return the array from $values
        } catch (error) {
            console.error('Error parsing JSON:', error);
            throw new Error('Failed to parse products data');
        }
    }
</script>



<style>
    .product-grid {
        display: grid;
        grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
        gap: 20px; /* Space between cards */
    }
</style>

<div class="flex flex-col lg:flex-row">
    <!-- Sidebar for Category Filter -->
    <div class="flex-0 p-4">
        <Card class="p-4 w-full shadow-md">
            <h2 class="text-xl font-semibold pb-1">Categories</h2>
            <div class="space-y-2">
                {#each categories as category}
                    <div
                            class="cursor-pointer px-4 py-2 rounded-full border text-sm font-semibold bg-gray-200 text-gray-700"
                            on:click={() => console.log(category.name)}
                    >
                        {category.name}
                    </div>
                {/each}
            </div>
        </Card>
    </div>
    <!-- Product Grid Section -->
    {#await getProducts()}
        <h2>Loading...</h2>
    {:then products}
        <div class="flex-1 p-4">
            <div class="product-grid grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-5">
                {#each products as product}
                    <Card padding="none">
                        <a href="/">
                            <img class="p-8 rounded-t-lg" src={product.image || '/images/default-product.jpg'} alt={product.title} />
                        </a>
                        <div class="px-5 pb-5">
                            <a href="/">
                                <h5 class="text-xl font-semibold tracking-tight text-gray-900 dark:text-white">{product.title}</h5>
                            </a>
                            <Rating rating={product.rating || 0} size={24} class="mt-2.5 mb-5">
                                <Badge slot="text" class="ms-3">{product.rating || 'N/A'}</Badge>
                            </Rating>
                            <div class="flex justify-between items-center">
                                <span class="text-3xl font-bold text-gray-900 dark:text-white">${product.price.toFixed(2)}</span>
                                <Button href="/">Buy now</Button>
                            </div>
                        </div>
                    </Card>
                {/each}
            </div>
        </div>
    {:catch error}
        <p>Error loading products: {error.message}</p>
    {/await}
</div>