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
    import {  HomeSolid, DropboxSolid, UserSettingsOutline, UserOutline, FileChartBarSolid } from 'flowbite-svelte-icons';
    import ProductList from "../components/ProductList.svelte";
    import ProductForm from "../components/ProductForm.svelte";
    import svelteLogo from '../assets/svelte.svg';
    import { SearchOutline } from 'flowbite-svelte-icons';
    import {Link} from "svelte-routing";
</script>


<Navbar class="px-4 sm:px-6 py-3 fixed w-full z-20 top-0 start-0 border-b border-gray-100 shadow-md bg-gradient-to-r from-blue-600 to-purple-600">
    <NavBrand href="/" class="hover:scale-105 transition-transform">
        <img src={svelteLogo} class="h-8 sm:h-10 mr-2 filter drop-shadow-lg" alt="Store Logo" />
        <span class="self-center text-2xl font-bold text-white drop-shadow-md">TechTrend</span>
    </NavBrand>
    
    <div class="flex items-center md:order-2 space-x-4">
        <!-- Enhanced Search -->
        <div class="hidden md:flex relative group">
            <div class="absolute inset-y-0 left-0 flex items-center pl-3 pointer-events-none">
                <SearchOutline class="w-5 h-5 text-gray-400 group-focus-within:text-blue-500 transition-colors" />
            </div>
            <Input 
                id="search-navbar" 
                class="pl-10 pr-4 py-2.5 rounded-full bg-white/90 backdrop-blur-sm border-0 focus:ring-2 focus:ring-white w-64 transition-all" 
                placeholder="Search products..." 
            />
        </div>

        <!-- Auth Buttons -->
        <div class="hidden md:flex space-x-2">
            <Link href="/component/register">
                <Button color="alternative" class="text-white hover:bg-white/10 px-4 py-2.5 rounded-full transition-colors">
                    <UserOutline class="w-5 h-5 mr-2" />
                    Sign Up
                </Button>
            </Link>
        </div>

        <NavHamburger class="text-white hover:bg-white/10 md:hidden" />
    </div>

    <NavUl class="md:space-x-1">
        <NavLi href="/" class="text-white hover:bg-white/10 px-4 py-2 rounded-lg transition-colors">
            <HomeSolid class="w-5 h-5 mr-2" />
            Home
        </NavLi>
        <NavLi href="/products" class="text-white hover:bg-white/10 px-4 py-2 rounded-lg transition-colors">
            <DropboxSolid class="w-5 h-5 mr-2" />
            Products
        </NavLi>
        <NavLi href="/admin" class="text-white hover:bg-white/10 px-4 py-2 rounded-lg transition-colors">
            <FileChartBarSolid class="w-5 h-5 mr-2" />
            Dashboard
        </NavLi>
        <NavLi href="/settings" class="text-white hover:bg-white/10 px-4 py-2 rounded-lg transition-colors">
            <UserSettingsOutline class="w-5 h-5 mr-2" />
            Settings
        </NavLi>
    </NavUl>
</Navbar>
