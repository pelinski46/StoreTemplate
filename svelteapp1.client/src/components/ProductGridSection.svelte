<script lang="ts">
    import { ArrowRight } from 'lucide-svelte';
    

    type ProductGridSectionProps = {
        title: string;
        products: Product[];
    };

    let { title, products }: ProductGridSectionProps = $props();
</script>

<div class="space-y-4">
    <div class="flex gap-4">
        <h2 class="text-3xl font-bold capitalize">{title}</h2>
        <button  class="space-x-2">
            <span> view all </span>
            <ArrowRight class="size-4" />
        </button>
    </div>
    <div class="grid grid-cols-1 gap-4 md:grid-cols-2 lg:grid-cols-3">
        {#each products as product}
           
        {/each}
    </div>
</div>