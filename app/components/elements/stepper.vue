<template>
  <div :is="container">
    <StepperHeader
      v-if="header"
      :value="value"
      :steps="steps"
      class="mb4"
      @input="goTo($event)" />
    <slot />
  </div>
</template>

<script>
  export default {
    name: "Stepper",
    props: {
      value: {
        type: Number,
        default: 1,
      },
      container: {
        type: String,
        default: "div",
      },
      header: {
        type: Boolean,
        default: false,
        required: false,
      },
    },
    data() {
      return {
        steps: [],
      };
    },
    mounted() {
      this.steps = this.$children.filter(a => a.$options.name === "Step")
        .map((a, i) => ({ id: i + 1, name: a.title }));
    },
    methods: {
      goTo(index) {
        if (this.value > index) {
          this.$emit("input", index);
        }
      },
    },
  };
</script>
