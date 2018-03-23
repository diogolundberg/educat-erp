<template>
  <div
    :class="{ 'x2 y2 p6x shadow1': active, 'bg-green': complete } "
    class="x1 y1 circle relative"
    @click="$emit('click')">
    <div
      :class="{ 'bg-green': active, 'bg-gray': !active }"
      class="block w100 h100 circle ease">
      <Icon
        v-if="complete"
        name="check" />
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
      title: {
        type: String,
        required: false,
        default: null,
      },
    },
  };
</script>
