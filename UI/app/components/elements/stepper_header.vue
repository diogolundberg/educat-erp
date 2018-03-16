<template>
  <div class="px4 flex items-center justify-between">
    <template v-for="step in steps">
      <StepperItem
        :active="value >= step.id"
        :complete="value > step.id"
        :title="step.name"
        :key="step.id"
        :class="{ pointer: value > step.id }"
        @click="$emit('input', step.id)" />
      <hr
        v-if="step.id < steps.length"
        :class="{ 'border-blue border2': value > step.id }"
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
        required: true,
      },
      steps: {
        type: Array,
        required: true,
      },
    },
  };
</script>
