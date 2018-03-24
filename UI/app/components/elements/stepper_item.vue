<template>
  <div
    :class="{
      'x2 y2 p6x shadow1': big,
      'bg-green': complete && !active,
      'bg-red': error && !active,
    }"
    class="x1 y1 circle relative"
    @click="$emit('click')">
    <div
      :class="{ 'bg-green': big, 'bg-red': error, 'bg-gray': !big }"
      class="block w100 h100 circle ease">
      <Icon
        v-if="complete && !active"
        name="check" />
      <Icon
        v-if="error && !active"
        name="error" />
    </div>
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
      title: {
        type: String,
        required: false,
        default: null,
      },
    },
    computed: {
      big() {
        return this.active || this.complete || this.error;
      },
    },
  };
</script>
