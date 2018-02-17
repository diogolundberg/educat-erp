<template>
  <a class="btn upcase raised rounded transition"
    @click="$emit('click')">
    {{ label }}
    <slot />
  </a>
</template>

<script>
  export default {
    name: "Btn",
    props: {
      label: {
        type: String,
        required: false,
        default: "",
      },
    },
  };
</script>
