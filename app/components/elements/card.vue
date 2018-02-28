<template>
  <div class="mb3 shadow0 bg-white">
    <template v-if="title">
      <div class="p3 bg-blue-grad white">
        <h2 class="m0 thin">{{ title }}</h2>
        <slot name="title" />
      </div>
    </template>
    <div class="p3">
      <slot />
    </div>
  </div>
</template>

<script>
  export default {
    name: "Card",
    props: {
      title: {
        type: String,
        required: false,
        default: null,
      },
    },
  };
</script>
