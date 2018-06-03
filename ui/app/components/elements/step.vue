<template>
  <div class="mb3">
    <StepHeader
      :index="index"
      :title="title"
      :description="description"
      :disabled="disabled"
      :complete="complete"
      :error="error"
      :warning="warning" />
    <div
      v-if="visible && !disabled"
      class="shadow1 rounded bg-silver p2">
      <div class="bg-white p2">
        <slot />
      </div>
    </div>
  </div>
</template>

<script>
  export default {
    name: "Step",
    props: {
      title: {
        type: String,
        required: true,
      },
      description: {
        type: String,
        required: true,
      },
      disabled: {
        type: Boolean,
        default: false,
      },
      complete: {
        type: Boolean,
        default: false,
      },
      error: {
        type: Boolean,
        default: false,
      },
      warning: {
        type: Boolean,
        default: false,
      },
    },
    computed: {
      visible() {
        return this.$parent.value === this.index;
      },
      index() {
        return this.$parent.$slots.default.filter(s => s.tag)
          .indexOf(this.$vnode) + 1;
      },
      current() {
        return this.$parent.value;
      },
    },
  };
</script>
