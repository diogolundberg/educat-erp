<template>
  <div :class="`mb3 col-12 sm-col-${size}`">
    <div class="relative">
      <input
        :value="value"
        disabled
        readonly
        class="m0 py2 ease border-none h5 w100 bg-transparent input-line">
      <label
        class="py2 absolute top-0 left-0 bounce h5 noclick nudge"
        v-text="label" />
      <template v-if="hint">
        <label class="block dim h7">{{ hint }}</label>
      </template>
    </div>
  </div>
</template>

<script>
  export default {
    name: "InfoBox",
    props: {
      value: {
        type: String,
        default: "",
      },
      label: {
        type: String,
        required: true,
      },
      hint: {
        type: String,
        required: false,
        default: null,
      },
      size: {
        type: Number,
        required: false,
        default: 3,
      },
    },
  };
</script>
