<template>
  <div>
    <slot />
  </div>
</template>

<script>
  export default {
    name: "Outside",
    mounted() {
      document.addEventListener("click", this.handler);
    },
    beforeDestroy() {
      document.removeEventListener("click", this.handler);
    },
    methods: {
      handler(e) {
        if (!this.$el.contains(e.target)) {
          this.$emit("click");
        }
      },
    },
  };
</script>
