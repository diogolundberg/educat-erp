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
    watch: {
      value(index) {
        this.$nextTick(() => {
          const el = this.$children.find(a => a.index === index);
          if (el && el.$el) {
            window.scrollTo({ top: el.$el.offsetTop - 60 });
          }
        });
      },
    },
    mounted() {
      this.steps = this.$children.filter(a => a.$options.name === "Step")
        .map((a, i) => ({ id: i + 1, name: a.title }));
    },
    methods: {
      goTo(index) {
        const { disabled } = this.$children.find(a => a.index === index);
        if (!disabled) {
          this.$emit("input", index);
        }
      },
    },
  };
</script>
