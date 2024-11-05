<script lang="ts">
    import type { Product } from "../Models/Product";
    import { Input, Label, Button, Modal } from 'flowbite-svelte';

    // Define a new product object with initial empty/default values
    let newProduct: Product = {
        title: '',
        description: '',
        quantity: 0,
        price: 0,
        categoryId: undefined,
        image: ''
    };

    // Define variables for category modal
    let newCategory: string = '';
    let isCategoryModalOpen = false;

    // Function to create a product
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
                // Clear the form after successful creation
                newProduct = {
                    title: '',
                    description: '',
                    quantity: 0,
                    price: 0,
                    categoryId: undefined,
                    image: ''
                };
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
    function addCategory() {
        console.log('New category added:', newCategory);
        // Logic to save the new category (e.g., API call)
        isCategoryModalOpen = false;
        newCategory = ''; // Reset the category input
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
        <div class="flex items-center space-x-2">
            <div>
                <Label for="categoryId" class="mb-2">Category ID</Label>
                <Input type="number" id="categoryId" bind:value={newProduct.categoryId} required placeholder="1" />
            </div>
            <Button type="button" on:click={openCategoryModal} class="mt-8">Add New Category</Button>
        </div>
        <div>
            <Label for="image" class="mb-2">Image URL</Label>
            <Input type="text" id="image" bind:value={newProduct.image} required placeholder="http://example.com/image.jpg" />
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
            <Input type="text" bind:value={newCategory} placeholder="Enter category name" required />
        </Label>
        <div class="flex justify-end">
            <Button type="button" on:click={() => (isCategoryModalOpen = false)} class="text-gray-500">Cancel</Button>
            <Button type="submit" class="ml-2">Add Category</Button>
        </div>
    </form>
</Modal>
