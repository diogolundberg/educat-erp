<template>
  <div class="relative">
    <div v-if="active" class="absolute fill circle spin spinner" />
    <div class="transition" :class="{ blur: active }">
      <slot />
    </div>
  </div>
</template>

<script>
  export default {
    name: "Spinner",
    props: {
      active: {
        type: Boolean,
        default: false,
      },
    },
  };
</script>
