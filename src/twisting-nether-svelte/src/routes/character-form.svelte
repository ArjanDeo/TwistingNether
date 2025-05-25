<script lang="ts">
    import * as Form from "$lib/components/ui/form"
    import { Input } from "$lib/components/ui/input";
    import { Toaster } from "$lib/components/ui/sonner";
	import * as Popover from "$lib/components/ui/popover";
	import * as Command from "$lib/components/ui/command";
    import * as Select from "$lib/components/ui/select/";
    import { characterFormSchema, type FormSchema } from "./characterFormSchema";
    import SuperDebug, { type SuperValidated, type Infer, superForm } from "sveltekit-superforms";
    import { zodClient } from "sveltekit-superforms/adapters";
	import { tick } from "svelte";
	import { useId } from "bits-ui";
	import { cn } from "$lib/utils";
	import { buttonVariants } from "$lib/components/ui/button";
	import { realms } from "$lib/realms";
	import { dev } from "$app/environment";
    import Fa from 'svelte-fa'
    import { toast } from 'svelte-sonner'
    import { faChevronDown } from '@fortawesome/free-solid-svg-icons'
	import Button from "$lib/components/ui/button/button.svelte";
    let { data }: { data: { form: SuperValidated<Infer<FormSchema>> } } =
        $props();

    const form = superForm(data.form, {
        validators: zodClient(characterFormSchema),
    });

    const { form: formData, enhance, allErrors, errors} = form;
    
    // Popover + Combobox shit
    let open = $state(false);

    function closeAndFocusTrigger(triggerId: string) {
    open = false;
    tick().then(() => {
      document.getElementById(triggerId)?.focus();
    });
  }
  const triggerId = useId();

 $effect(() => {
    if ($allErrors.length) {
        if ($allErrors[0].messages[0].includes('Character')) {
        toast.error($allErrors[0].messages[0])
        }
    }
 })
</script>
{#if dev}
<div class="mb-2">
    <SuperDebug data={formData} />
</div>
{/if}
<form method="POST" use:enhance>
    <Form.Field {form} name="name" class="w-full">
        <Form.FieldErrors />
        <Form.Control>
            {#snippet children({ props })}
                <Input {...props} bind:value={$formData.name} placeholder="Character Name"/>
            {/snippet}
        </Form.Control>
    </Form.Field>
    <div class="flex flex-row items-start gap-2">
        <Form.Field {form} name="realm">
        <Form.FieldErrors />
                <Popover.Root bind:open>
            <Form.Control id={triggerId}>
                {#snippet children({ props })}
                <Popover.Trigger class={cn(buttonVariants({ variant: "outline" }), "w-56  justify-between cursor-pointer", !$formData.realm && "text-muted-foreground")} role="combobox" {...props}>
                    {$formData.realm != "" ? $formData.realm : "Select realm"}
                    <Fa icon={faChevronDown} />
                </Popover.Trigger>
                <input hidden value={$formData.realm} name={props.name} />
                {/snippet}
            </Form.Control>
            <Popover.Content class="w-[200px] p-0">
                <Command.Root>
                <Command.Input
                    autofocus
                    placeholder="Search realms..."
                    class="h-9"
                />
                <Command.Empty>No language found.</Command.Empty>
                <Command.List>
                    <Command.Group value="realms">
                        {#each realms as realm (realm.realmName)}
                        <Command.Item
                            value={realm.realmName}
                            onSelect={() => {
                            $formData.realm = realm.realmName;
                            closeAndFocusTrigger(triggerId);
                            }}
                            class="cursor-pointer"
                        >
                            {realm.realmName}
                        </Command.Item>
                        {/each}
                    </Command.Group>
                </Command.List>
                </Command.Root>
            </Popover.Content>
            </Popover.Root>
            </Form.Field>
            <Form.Field {form} name="region">
            <Form.FieldErrors />
                <Form.Control>
                    {#snippet children({ props })}
                        <Select.Root type="single" bind:value={$formData.region} name={props.name} >
                            <Select.Trigger {...props} class="w-20 bg-white cursor-pointer">
                                {$formData.region
                                ? $formData.region
                                : "Select the characters region"}
                            </Select.Trigger>
                            <Select.Content>
                                <Select.Item value="US" label="US" />
                                <Select.Item value="EU" label="EU" />
                                <Select.Item value="KR" label="KR" />
                                <Select.Item value="TW" label="TW" />
                                <Select.Item value="CN" label="CN" />
                            </Select.Content>
                        </Select.Root>
                    {/snippet}
                </Form.Control>
                <Form.FieldErrors />
        </Form.Field>
    </div>    
    <Form.Button class="hover:cursor-pointer">Search</Form.Button>{#if $allErrors.length}
{/if}
</form>