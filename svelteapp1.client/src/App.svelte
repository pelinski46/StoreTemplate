<script>

import "./app.css";
import { Router, Route } from 'svelte-routing';
import Home from './pages/Home.svelte';
import Login from './pages/Login.svelte';
import Register from './components/Register.svelte';
import AuthorizeView from './AuthorizeView.svelte';
import ProductList from "./components/ProductList.svelte";
import Layout from "./components/Layout.svelte";
import ProductForm from "./components/ProductForm.svelte";
</script>


<Router>
  <Layout>
    <div class="pt-5 pl-2 pr-2">
      <Route component={Home} />
      <Route path="/component/login" component={Login}/>
      <Route path="/component/register" component={Register}/>
      <Route path="/products" component={ProductList}/>
      <Route path="/admin" component={ProductForm}/>
    </div>
  </Layout>
</Router>


<style>
  /* app.css or a specific style for your layout */
  .mt-4 {
    margin-top: 2rem; /* Adjust this value as needed */
  }

  .space-y-20 > * + * {
    margin-top: 20px; /* Space between components */
  }
</style>