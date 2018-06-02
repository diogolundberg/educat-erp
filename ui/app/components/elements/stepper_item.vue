<template>
  <div
    :class="{
      'bg-green': complete && !active,
      'bg-red': error && !active,
      'bg-yellow': warning && !active,
    }"
    class="x1 y1 circle relative"
    @click="$emit('click')">
    <div
      :class="{
        'bg-green': complete,
        'bg-red': error,
        'bg-yellow': warning,
      }"
      class="block w100 h100 bg-gray circle ease" />
    <div class="absolute left2n x6 pt2 h7 center xs-hide">
      {{ title }}
    </div>
  </div>
</template>

<script>
  export default {
    name: "StepperItem",
    props: {
      active: {
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
      title: {
        type: String,
        required: false,
        default: null,
      },
    },
  };
</script>
