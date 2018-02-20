<template>
  <div class="shadow1 rounded bg-white p2">
    <h2 class="m0 mb1 thin" v-if="title">
      {{ title }}
    </h2>
    <slot />
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
