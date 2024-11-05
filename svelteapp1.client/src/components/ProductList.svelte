<script lang="ts">
    import { Card, Button, Rating, Badge } from 'flowbite-svelte';

    // Function to fetch product data from the API
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
    /* Add some styling for the product list */
    .product-grid {
        display: grid;
        grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
        gap: 20px; /* Space between cards */
    }
    h1 {
        margin-top: 20px; /* or any other value you see fit */
    }
</style>

{#await getProducts()}
    <h2>Loading...</h2>
{:then products}
    <div class="product-grid">
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
{:catch error}
    <p>Error loading products: {error.message}</p>
{/await}
