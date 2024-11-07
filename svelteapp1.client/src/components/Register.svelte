<script lang="ts">
    import { navigate } from "svelte-routing";
    import {Button, Input, Label} from "flowbite-svelte";
    import { EyeOutline, EyeSlashOutline, EnvelopeSolid } from 'flowbite-svelte-icons';

    let showPassword = false;
    let showConfirmPassword = false;
    let email: string = "";
    let password: string = "";
    let confirm: string = "";
    let message: string = "";
    let valid: boolean = false;

    // A simple function to check if an email is valid

    function handleLogin() {
        navigate("/component/login");
    }

    function validateEmail(email: string) {
        const regex = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
        return regex.test(email);
    }

    // A function to check if the form inputs are valid and set the message accordingly
    function validateForm() {
        if (!email || !password || !confirm) {
            message = "Please fill in all the fields.";
            valid = false;
        } else if (!validateEmail(email)) {
            message = "Please enter a valid email address.";
            valid = false;
        } else if (password !== confirm) {
            message = "The passwords do not match.";
            valid = false;
        } else {
            message = "The form is valid.";
            valid = true;
        }
    }

    // A function to submit the form data to the /register api as a json object
    async function submitForm() {
        validateForm();
        if (valid) {
            const data = { email, password };
            const response = await fetch("/register", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(data),
            });
            const result = await response;
            if (response.ok) message = "Success.";
            else message = "Error registering.";
        }
    }
</script>

<div class="box">
    <form on:submit|preventDefault={submitForm}>
        <div class="space-y-4">

            <!-- Email Field with Envelope Icon -->
            <div>
                <Label for="email">Email:</Label>
                <Input id="email" type="email" placeholder="Enter your email" bind:value={email} size="lg" on:input={validateForm}>
                    <EnvelopeSolid slot="left" class="w-5 h-5" />
                </Input>
            </div>

            <!-- Password Field with Visibility Toggle -->
            <div>
                <Label for="password">Password:</Label>
                <Input id="password" type={showPassword ? 'text' : 'password'} placeholder="Enter your password" bind:value={password} size="lg" on:input={validateForm}>
                    <button slot="right" on:click={() => (showPassword = !showPassword)} type="button" class="pointer-events-auto">
                        {#if showPassword}
                            <EyeOutline class="w-5 h-5" />
                        {:else}
                            <EyeSlashOutline class="w-5 h-5" />
                        {/if}
                    </button>
                </Input>
            </div>

            <!-- Confirm Password Field with Visibility Toggle -->
            <div>
                <Label for="confirm">Password (Confirm):</Label>
                <Input id="confirm" type={showConfirmPassword ? 'text' : 'password'} placeholder="Confirm your password" bind:value={confirm} size="lg" on:input={validateForm}>
                    <button slot="right" on:click={() => (showConfirmPassword = !showConfirmPassword)} type="button" class="pointer-events-auto">
                        {#if showConfirmPassword}
                            <EyeOutline class="w-5 h-5" />
                        {:else}
                            <EyeSlashOutline class="w-5 h-5" />
                        {/if}
                    </button>
                </Input>
            </div>

            <!-- Submit Button -->
            <Button type="submit" class="w-full mt-4">Register</Button>

            <!-- Message Display -->
            {#if message}
                <p class="text-red-500">{message}</p>
            {/if}
        </div>
    </form>

    <!-- Go to Login Button -->
    <Button on:click={handleLogin} class="mt-4">Go to Login</Button>
</div>