<template>
  <div class="mb3 shadow2 bg-white">
    <div class="p3 bg-blue white" v-if="title">
      <h2 class="m0 thin">{{ title }}</h2>
      <slot name="title" />
    </div>
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
      },
    },
  };
</script>
