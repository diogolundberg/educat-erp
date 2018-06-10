<template>
  <div class="mb3">
    <StepHeader
      :active="active"
      :index="index"
      :title="title"
      :description="description"
      :disabled="disabled"
      :complete="status === 'valid'"
      :error="status === 'invalid'"
      :warning="status === 'pending'"
      @click="$parent.goTo(index)" />
    <div
      v-if="active && !disabled"
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
      status: {
        type: String,
        default: "empty",
      },
    },
    computed: {
      active() {
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
