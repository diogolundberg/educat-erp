<template>
  <div class="shadow2 bg-white">
    <h2 class="m0 p3 bg-blue thin white" v-if="title">
      {{ title }}
    </h2>
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
