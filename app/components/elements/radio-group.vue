<template>
  <span :is="container">
    <span :is="container" v-for="option in options" :key="option[idField]"
      @click="$emit('input', option[idField])" class="pointer my2"
      role="radiogroup">
      <span class="inline-block align-middle x2 y2 p4x circle bg-white shadow0"
        role="radio" :aria-checked="option[idField] == value">
        <div class="block w100 h100 circle bg-blue transition"
          v-if="option[idField] == value" />
      </span>
      <label class="mx1 bold upcase pointer">
        {{ option[labelField] }}
      </label>
    </span>
  </span>
</template>

<script>
  export default {
    name: "RadioGroup",
    props: {
      value: {
        type: [Number, String],
        default: null,
      },
      options: {
        type: Array,
        default: () => [],
      },
      idField: {
        type: String,
        default: "id",
      },
      labelField: {
        type: String,
        default: "name",
      },
      container: {
        type: String,
        default: "span",
      },
    },
  };
</script>
