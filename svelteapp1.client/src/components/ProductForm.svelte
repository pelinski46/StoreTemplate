<script lang="ts">
    import type { Product } from "../Models/Product";
    import {Input, Label, Button, Modal, Select, Fileupload, Helper} from 'flowbite-svelte';
    import type {Category} from "../Models/Category";
    import {onMount} from "svelte";

    // Define a new product object with initial empty/default values
    let newProduct: Product = {
        title: '',
        description: '',
        quantity: 0,
        price: 0,
        categoryId: undefined,
        image: ''
    };

    let categories: Category[] = [];
    let newCategory: Category = { name: '' };
    let isCategoryModalOpen = false;

    
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
    
    async function createProduct(newProduct: Product) {
        try {
            const response = await fetch('/Product', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(newProduct),
            });
            if (response.ok) {
                const createdProduct = await response.json();
                console.log('Product created:', createdProduct);
                newProduct = {
                    title: '',
                    description: '',
                    quantity: 0,
                    price: 0,
                    categoryId: undefined,
                    image: ''
                };
                
                // Force reactivity update
                $: newProduct = newProduct;
            } else {
                console.error('Failed to create product');
            }
        } catch (error) {
            console.error('Error:', error);
        }
    }

    // Handle form submission
    function handleSubmit(event: Event) {
        event.preventDefault();
        createProduct(newProduct);
    }

    // Function to open the category modal
    function openCategoryModal() {
        isCategoryModalOpen = true;
    }
    
    // Function to add a new category
   async function addCategory(event: SubmitEvent) {
        try {
            const response = await fetch('/Categories', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(newCategory),
            });
            if (response.ok) {
                const createdCategory = await response.json();
                console.log('New category added:', createdCategory);
                categories = await fetchCategories();
                isCategoryModalOpen = false;
               
           
            } else {
                console.error('Failed to create product');
            }
        } catch (error) {
            console.error('Error:', error);
        }
    }

    function handleImageUpload(event: Event) {
        const fileInput = event.target as HTMLInputElement;
        const file = fileInput?.files?.[0];
        if (file) {
            const reader = new FileReader();
            reader.onloadend = () => {
                newProduct.image = reader.result as string; // Set Base64 string
            };
            reader.readAsDataURL(file); // Convert file to Base64
        }
    }
</script>

<h1>Create a New Product</h1>

<form on:submit|preventDefault={handleSubmit}>
    <div class="grid gap-6 mb-6 md:grid-cols-2">
        <div>
            <Label for="title" class="mb-2">Title</Label>
            <Input type="text" id="title" bind:value={newProduct.title} required placeholder="Product Title" />
        </div>
        <div>
            <Label for="description" class="mb-2">Description</Label>
            <Input id="description" bind:value={newProduct.description} required placeholder="Product Description"></Input>
        </div>
        <div>
            <Label for="quantity" class="mb-2">Quantity</Label>
            <Input type="number" id="quantity" bind:value={newProduct.quantity} required placeholder="0" />
        </div>
        <div>
            <Label for="price" class="mb-2">Price</Label>
            <Input type="number" step="0.01" id="price" bind:value={newProduct.price} required placeholder="0.00" />
        </div>

        <div>
            <Label for="image" class="mb-2">Image</Label>
            <Fileupload
                    id="image"
                    class="mb-2"
                    accept="image/*"
                    on:change={handleImageUpload}
            />
            <Helper>SVG, PNG, JPG or GIF (MAX. 800x400px).</Helper>
            {#if newProduct.image}
                <img src={newProduct.image} alt="Product Image Preview" class="mt-2" />
            {/if}
        </div>
        <div class="flex items-center space-x-2">
            <div>
                <Label for="categoryId" class="mb-2">Category</Label>
                <Select
                        id="categoryId"
                        bind:value={newProduct.categoryId}
                        required
                        placeholder="Select a Category"
                >
                    
                    {#each categories as category}
                        <option value={category.id}>{category.name}</option>
                    {/each}
                </Select>
            </div>
            <Button type="button" on:click={openCategoryModal} class="mt-8">Add New Category</Button>
        </div>
        
    </div>

    <Button type="submit" class="mt-4">Create Product</Button>
</form>

<!-- Category Modal -->
<Modal bind:open={isCategoryModalOpen} size="xs" autoclose={false}>
    <form class="flex flex-col space-y-6" on:submit|preventDefault={addCategory}>
        <h3 class="mb-4 text-xl font-medium text-gray-900 dark:text-white">Add a New Category</h3>
        <Label class="space-y-2">
            <span>Category Name</span>
            <Input type="text" bind:value={newCategory.name} placeholder="Enter category name" required />
        </Label>
        <div class="flex justify-end">
            <Button type="button" on:click={() => (isCategoryModalOpen = false)} class="text-gray-500">Cancel</Button>
            <Button type="submit" class="ml-2">Add Category</Button>
        </div>
    </form>
</Modal>
