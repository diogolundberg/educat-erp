<template>
  <div class="p2 px4 flex items-center justify-between shadow0">
    <template
      v-for="(step, index) in steps">
      <Ball
        :key="step.id"
        :index="step.id"
        :blank="value < step.id"
        :data-tip="step.name" />
      <hr
        v-if="step.id < steps.length"
        :class="{ 'border-blue border2': value > step.id }"
        class="flex-auto border-bottom" />
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
