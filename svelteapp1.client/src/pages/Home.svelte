<script lang='ts'>
    import svelteLogo from '../assets/svelte.svg'
    import Counter from '../components/Counter.svelte'
    import WeatherForecast from '../components/WeatherForecast.svelte';
    import { Link } from 'svelte-routing';
    import AuthorizeView from '../AuthorizeView.svelte';
    import Logout from '../components/Logout.svelte';
    import ProductGridSection from "../components/ProductGridSection.svelte";

</script>


<main class="space-y-12">
    <ProductGridSection title="most popular" products={data.mostPopularProducts} />
    <ProductGridSection title="newest" products={data.newestProducts} />
</main>