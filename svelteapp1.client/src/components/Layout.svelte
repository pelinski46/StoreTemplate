<script>
    import MenuBar from "../components/MenuBar.svelte";
</script>

<div class="fixed top-0 w-full z-50">
    <MenuBar />
</div>
<div class="mt-16"> <!-- Adjust mt-16 based on the height of your MenuBar -->
    <slot />
</div>
