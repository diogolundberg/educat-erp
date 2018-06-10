<template>
  <div
    class="fixed fill-h px4 pt2 pb4 flex items-center justify-between bg-white
      shadow2 z2">
    <template v-for="step in steps">
      <StepperItem
        :active="value == step.id"
        :complete="step.el.status === 'valid'"
        :warning="step.el.status === 'invalid'"
        :error="step.el.status === 'pending'"
        :title="step.name"
        :key="step.id"
        class="pointer"
        @click="$emit('input', step.id)" />
      <hr
        v-if="step.id < steps.length"
        :key="step.id + '_separator'"
        class="flex-auto border-bottom">
    </template>
  </div>
</template>

<script>
  export default {
    name: "StepperHeader",
    props: {
      value: {
        type: [String, Number],
        required: false,
        default: null,
      },
      steps: {
        type: Array,
        required: true,
      },
    },
  };
</script>
