<template>
  <div
    :class="{ 'x2 y2 p6x shadow2': active, 'bg-blue': complete } "
    class="x1 y1 circle relative"
    @click="$emit('click')">
    <div
      :class="{ 'bg-blue': active, 'bg-gray': !active }"
      class="block w100 h100 circle ease">
      <svg
        v-if="complete"
        viewBox="0 0 512 512">
        <path
          d="m208 433l-164-164 77-78 87 85 196-197 79 78z"
          fill="#fff" />
      </svg>
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
