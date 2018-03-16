<template>
  <div v-if="visible">
    <slot />
  </div>
</template>

<script>
  export default {
    name: "Tab",
    props: {
      title: {
        type: String,
        required: true,
      },
    },
    computed: {
      visible() {
        return this.$parent.current === this;
      },
    },
  };
</script>
