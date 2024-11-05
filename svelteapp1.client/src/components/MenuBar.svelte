<script>
    import { Navbar, NavBrand, NavLi, NavUl, NavHamburger, ImagePlaceholder, Skeleton, TextPlaceholder } from 'flowbite-svelte';
    import ProductList from "../components/ProductList.svelte";
    import ProductForm from "../components/ProductForm.svelte";
    import svelteLogo from '../assets/svelte.svg'
    import {Link} from "svelte-routing";
</script>


<Navbar class="px-2 sm:px-4 py-2.5 fixed w-full z-20 top-0 start-0 border-b mb-4">
    <NavBrand href="/">
        <img src={svelteLogo} class="me-3 h-6 sm:h-9" alt="Flowbite Logo" />
        <span class="self-center whitespace-nowrap text-xl font-semibold dark:text-white">Tienda</span>
    </NavBrand>
    <NavHamburger />
    <NavUl>
        <NavLi href="/">Inicio</NavLi>
        <NavLi href="/products">Productos</NavLi>
        <NavLi href="/admin">Administrador</NavLi>
        <NavLi href="/component/register">Registrarse</NavLi>
    </NavUl>
</Navbar>
