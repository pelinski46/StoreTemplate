import { mount } from 'svelte';
import './app.css';
import App from './App.svelte';

// Mount the app using the new Svelte 5 API
const app = mount(App, {
  target: document.getElementById('app') as HTMLElement,
});

export default app;
