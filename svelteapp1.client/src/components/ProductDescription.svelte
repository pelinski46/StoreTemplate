<script lang="ts">
    import { onMount } from 'svelte';
    import { Card, Rating, Badge, Button } from 'flowbite-svelte';
    import type {Product} from "../Models/Product";
  

    let product: Product;
    let productId: string | undefined;

    // Get productId from the URL
    onMount(() => {
        const url = new URL(window.location.href);
        productId = url.pathname.split('/').pop(); // Assuming the URL is structured like /product/{id}
        fetchProduct();
    });

    // Function to fetch a specific product by its ID
    async function fetchProduct() {
        if (!productId) return;
        console.log(`Fetching product with ID: ${productId}`);
        const response = await fetch(`/Product/${productId}`);

        if (!response.ok) {
            throw new Error('Failed to fetch product');
        }

        try {
            product = await response.json(); 
            console.log('Fetched product:', product);
        } catch (error) {
            console.error('Error parsing JSON:', error);
            throw new Error('Failed to parse product data');
        }
    }
</script>

{#if product}
    <div class="bg-white">
        <!-- Product Image Gallery -->
        <div class="mx-auto mt-6 max-w-2xl sm:px-6 lg:grid lg:max-w-7xl lg:grid-cols-3 lg:gap-x-8 lg:px-8">
            <div class="aspect-h-4 aspect-w-3 hidden overflow-hidden rounded-lg lg:block">
                <img src={product.image || '/images/default-product.jpg'} alt={product.title} class="h-full w-full object-cover object-center">
            </div>
        </div>

        <!-- Product Info -->
        <div class="mx-auto max-w-2xl px-4 pb-16 pt-10 sm:px-6 lg:grid lg:max-w-7xl lg:grid-cols-3 lg:grid-rows-[auto,auto,1fr] lg:gap-x-8 lg:px-8 lg:pb-24 lg:pt-16">
            <div class="lg:col-span-2 lg:border-r lg:border-gray-200 lg:pr-8">
                <h1 class="text-2xl font-bold tracking-tight text-gray-900 sm:text-3xl">{product.title}</h1>
                <p class="text-lg font-semibold text-gray-500 mt-2">Price: ${product.price?.toFixed(2) ?? 'N/A'}</p>
                <p class="text-gray-600 mt-4">{product.description || 'No description available.'}</p>
            </div>

            <!-- Product Options -->
            <div class="mt-4 lg:mt-0">
                <h2 class="sr-only">Product information</h2>
                <form class="mt-10">
                    <!-- Color Choices (optional) -->
                    <div>
                        <h3 class="text-sm font-medium text-gray-900">Color</h3>
                        <fieldset aria-label="Choose a color" class="mt-4">
                            <div class="flex items-center space-x-3">
                                <!-- Add dynamic color options here based on product data if available -->
                                <label class="relative flex cursor-pointer items-center justify-center rounded-full p-0.5 ring-gray-400 focus:outline-none">
                                    <input type="radio" name="color-choice" value="White" class="sr-only">
                                    <span class="h-8 w-8 rounded-full bg-white"></span>
                                </label>
                                <label class="relative flex cursor-pointer items-center justify-center rounded-full p-0.5 ring-gray-400 focus:outline-none">
                                    <input type="radio" name="color-choice" value="Gray" class="sr-only">
                                    <span class="h-8 w-8 rounded-full bg-gray-200"></span>
                                </label>
                                <label class="relative flex cursor-pointer items-center justify-center rounded-full p-0.5 ring-gray-400 focus:outline-none">
                                    <input type="radio" name="color-choice" value="Black" class="sr-only">
                                    <span class="h-8 w-8 rounded-full bg-gray-900"></span>
                                </label>
                            </div>
                        </fieldset>
                    </div>

                    <!-- Size Choices -->
                    <div class="mt-10">
                        <h3 class="text-sm font-medium text-gray-900">Size</h3>
                        <fieldset aria-label="Choose a size" class="mt-4">
                            <div class="grid grid-cols-4 gap-4 sm:grid-cols-8 lg:grid-cols-4">
                                <label class="group relative flex cursor-pointer items-center justify-center rounded-md border bg-white px-4 py-3 text-sm font-medium uppercase text-gray-900 shadow-sm hover:bg-gray-50">
                                    <input type="radio" name="size-choice" value="M" class="sr-only">
                                    <span>M</span>
                                </label>
                                <label class="group relative flex cursor-pointer items-center justify-center rounded-md border bg-white px-4 py-3 text-sm font-medium uppercase text-gray-900 shadow-sm hover:bg-gray-50">
                                    <input type="radio" name="size-choice" value="L" class="sr-only">
                                    <span>L</span>
                                </label>
                            </div>
                        </fieldset>
                    </div>

                    <!-- Add to Cart Button -->
                    <Button href="/" class="mt-6 w-full">Add to Cart</Button>
                </form>
            </div>
        </div>
    </div>
{:else}
    <h2>Loading...</h2>
{/if}

<style>
    
</style>
