<script>
// @ts-nocheck
import "./app.css";
import { Router, Route } from 'svelte-routing';
import Home from './pages/Home.svelte';
import Login from './pages/Login.svelte';
import Register from './pages/Register.svelte';
import AuthorizeView from './AuthorizeView.svelte';</script>


  <Router>
    <Route component="{Home}"></Route>
    <Route path="/login" component="{Login}/">
    <Route path="/register" component="{Register}/">
  </Route></Route></Router>


