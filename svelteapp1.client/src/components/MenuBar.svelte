<script>
    import {
        Navbar,
        NavBrand,
        NavLi,
        NavUl,
        NavHamburger,
        ImagePlaceholder,
        Skeleton,
        TextPlaceholder,
        Button, Input
    } from 'flowbite-svelte';
    import ProductList from "../components/ProductList.svelte";
    import ProductForm from "../components/ProductForm.svelte";
    import svelteLogo from '../assets/svelte.svg';
    import { SearchOutline } from 'flowbite-svelte-icons';
    import {Link} from "svelte-routing";
</script>


<Navbar class="px-2 sm:px-4 py-2.5 fixed w-full z-20 top-0 start-0 border-b mb-4">
    <NavBrand href="/">
        <img src={svelteLogo} class="me-3 h-6 sm:h-9" alt="Flowbite Logo" />
        <span class="self-center whitespace-nowrap text-xl font-semibold dark:text-white">Tienda</span>
    </NavBrand>
    <div class="flex md:order-2">
        <Button color="none" data-collapse-toggle="mobile-menu-3" aria-controls="mobile-menu-3" aria-expanded="false" class="md:hidden text-gray-500 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-700 focus:outline-none focus:ring-4 focus:ring-gray-200 dark:focus:ring-gray-700 rounded-lg text-sm p-2.5 me-1">
            <SearchOutline class="w-5 h-5" />
        </Button>
        <div class="hidden relative md:block">
            <div class="flex absolute inset-y-0 start-0 items-center ps-3 pointer-events-none">
                <SearchOutline class="w-4 h-4" />
            </div>
            <Input id="search-navbar" class="ps-10" placeholder="Search..." />
        </div>
        <NavHamburger />
    </div>
    <NavUl>
        <NavLi href="/">Inicio</NavLi>
        <NavLi href="/products">Productos</NavLi>
        <NavLi href="/admin">Administrador</NavLi>
        <NavLi href="/component/register">Registrarse</NavLi>
    </NavUl>
</Navbar>
